/* TENANT CREATION REQUEST RELATED */

TRUNCATE dws_tenant_creation_requests

SELECT * FROM dws_tenant_creation_requests

INSERT INTO dws_tenant_creation_requests (uniquename, token, reqdata, status) VALUES ('my_first_tenant','1022097D-54EB-4A4C-945C-CDC66D056562', '{"name":"my_first_tenant", "properName": "My First Tenant", "setupAdmin": "admin@myfirsttenant.com"}', 'Pending')

SELECT token FROM dws_tenant_creation_requests WHERE uniquename = 'my_first_tenant' AND status = 'Pending'

SELECT uniquename, status FROM dws_tenant_creation_requests WHERE token = '00b3e441-83f3-4a02-af9b-656f9b186e49'

/* TENANT RELATED */
TRUNCATE dws_tenants

SELECT * FROM dws_tenants

SELECT uid FROM dws_tenants WHERE uniquename = 'my_first_tenant'

INSERT INTO dws_tenants (uniquename, uid, propername, setupadmin, state) VALUES ('my_first_tenant', '39ae6920-7f70-4b3d-9b31-0f899a6fc317', 'My First Tenant', 'admin@myfirsttenant.com', 'Active')
