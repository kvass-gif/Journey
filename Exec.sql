use TestDb

EXEC SelectAllUsersByRole @RoleName = 'Tenant';
EXEC SelectAllUsersByRole @RoleName = 'Landlord';

Delete from AspNetUsers;