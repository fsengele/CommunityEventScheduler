
SELECT
    u.Email,
    r.Name AS RoleName
FROM AspNetUsers AS u
INNER JOIN AspNetUserRoles AS ur
    ON u.Id = ur.UserId
INNER JOIN AspNetRoles AS r
    ON ur.RoleId = r.Id
WHERE r.Name = 'Admin';
