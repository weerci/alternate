/* INSERT INTO sys.database_permissions
SELECT dp.class, dp.class_desc, dp.major_id, dp.minor_id, dp.grantee_principal_id,
       dp.grantor_principal_id, dp.[type], dp.permission_name, dp.[state],
       dp.state_desc
FROM   sys.database_permissions dp
WHERE  dp.grantee_principal_id = (
           SELECT sp.principal_id
           FROM   sys.database_principals sp
           WHERE  sp.name = 'web'
       )
       AND dp.major_id > 0 */




