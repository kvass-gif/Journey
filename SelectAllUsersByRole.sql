CREATE PROCEDURE SelectAllUsersByRole @RoleName nvarchar(30)
AS
 SELECT UserName, Name
  FROM AspNetRoles
  INNER JOIN AspNetUserRoles
  ON AspNetRoles.Id = AspNetUserRoles.RoleId
  AND AspNetRoles.Name = @RoleName
  INNER JOIN AspNetUsers
  ON AspNetUsers.Id = AspNetUserRoles.UserId
  ORDER BY UserName;

