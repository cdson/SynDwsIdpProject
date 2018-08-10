/* Define getutcdate function */
CREATE OR REPLACE FUNCTION getutcdate()
  RETURNS TIMESTAMP WITHOUT TIME ZONE AS $BODY$SELECT CURRENT_TIMESTAMP AT TIME ZONE 'UTC'$BODY$
LANGUAGE SQL VOLATILE COST 100;

ALTER FUNCTION getutcdate()
OWNER TO dws_svr_svc;

/* Create tenants table */
CREATE TABLE dws_tenants
(
  iid        BIGSERIAL                   NOT NULL,
  uniquename VARCHAR(512)                NOT NULL,
  uid        VARCHAR(256)                NOT NULL,
  propername VARCHAR(512)                NOT NULL,
  setupadmin VARCHAR(512)                NOT NULL,
  state      VARCHAR(50)                 NOT NULL,
  created    TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT getutcdate(),
  modified   TIMESTAMP WITHOUT TIME ZONE NULL,
  CONSTRAINT pk_tenant PRIMARY KEY (iid)
);

ALTER TABLE dws_tenants
  OWNER TO dws_svr_svc;

CREATE UNIQUE INDEX ix_tenant_uid
  ON dws_tenants (uid);

CREATE UNIQUE INDEX ix_tenant_uniquename
  ON dws_tenants (uniquename);

CREATE INDEX ix_tenant_state
  ON dws_tenants (state);

/* Tenant creation requests table */  
CREATE TABLE dws_request_queue
(
	id  bigserial NOT NULL PRIMARY KEY,
	token      UUID                        NOT NULL,
	tenantname VARCHAR(512)                NOT NULL,
	type       VARCHAR(50)                NOT NULL,
	reqdata    VARCHAR(2048)               NOT NULL,
	status     VARCHAR(128)                NOT NULL,
	message    VARCHAR(4096),
	islocked BOOLEAN,
	locktime timestamp without time zone,
	created    TIMESTAMP WITHOUT TIME ZONE NOT NULL DEFAULT getutcdate(),
	modified   TIMESTAMP WITHOUT TIME ZONE NULL
);

ALTER TABLE dws_request_queue
    OWNER to dws_svr_svc;

CREATE INDEX ix_request_queue_status
    ON dws_request_queue (status);

CREATE UNIQUE INDEX ix_request_queue_token
    ON dws_request_queue (token);
    
 
/* Last modified timestamp function & triggers */  
CREATE FUNCTION update_modified_column()
RETURNS TRIGGER AS $$
BEGIN
	NEW.modified = getutcdate();
	RETURN NEW;
END;
$$ language 'plpgsql';

CREATE TRIGGER update_request_modtime
    BEFORE UPDATE 
    ON dws_request_queue
    FOR EACH ROW
    EXECUTE PROCEDURE update_modified_column();

CREATE TRIGGER update_tenants_modtime
BEFORE UPDATE ON dws_tenants
FOR EACH ROW EXECUTE PROCEDURE update_modified_column();

/* create dws_tenant_subscriptions table*/
CREATE TABLE dws_tenant_subscriptions
(
	subscriptionId character varying(256) COLLATE pg_catalog."default" NOT NULL,
    tenantid character varying(512) COLLATE pg_catalog."default" NOT NULL,
    sku character varying(512) COLLATE pg_catalog."default" NOT NULL,
    seatCount numeric NOT NULL,
    isdeleted BOOLEAN NOT NULL,
	created timestamp without time zone NOT NULL DEFAULT getutcdate(),
    deleted timestamp without time zone,
    CONSTRAINT pk_dws_tenant_subscriptions PRIMARY KEY (subscriptionId),
    CONSTRAINT fk_dws_tenant_subscriptions_tenants FOREIGN KEY (tenantid)
        REFERENCES dws_tenants (uid) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;
ALTER TABLE dws_tenant_subscriptions
OWNER to dws_svr_svc;

/* create dws_solution_tenant table*/

CREATE TABLE dws_solution_tenant
(
	id  bigserial NOT NULL,
	solutionporivderid VARCHAR(512) NOT NULL,
    tenantid VARCHAR(256) NOT NULL,
    solutiontenantid VARCHAR(512) NOT NULL,
	created timestamp without time zone NOT NULL DEFAULT getutcdate(),
    CONSTRAINT pk_dws_solution_tenant PRIMARY KEY (id),
    CONSTRAINT fk_dws_solution_tenant_tenants FOREIGN KEY (tenantid)
        REFERENCES dws_tenants (uid) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)
WITH (
    OIDS = FALSE
)
TABLESPACE pg_default;
ALTER TABLE dws_solution_tenant
OWNER to dws_svr_svc;