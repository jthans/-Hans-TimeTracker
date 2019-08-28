CREATE PROCEDURE `sp_GetRecentTimeSummary` 
(
	IN EndDate		DATETIME,
	IN Org 			VARCHAR(255),
    IN StartDate	DATETIME,
    IN UserList 	VARCHAR(255)
)
BEGIN

SELECT ProjectId,
	   P.Description,
	   CONCAT(FLOOR(SUM(TotalMinutes) / 60),
				':', 
                LPAD(FLOOR(SUM(TotalMinutes) % 60), 2, '0')) AS TotalTime
   FROM (SELECT ProjectId, UserId, TIMESTAMPDIFF(MINUTE, TimeStart, TimeEnd) AS TotalMinutes 
			FROM ProjectData
            WHERE (ISNULL(StartDate) OR (TimeStart > StartDate))
			  AND (ISNULL(EndDate) OR (TimeEnd < EndDate))) PD
	INNER JOIN Projects P ON P.Id = PD.ProjectID
    INNER JOIN User U ON U.Id = PD.UserId
   WHERE (ISNULL(UserList) OR UserList LIKE CONCAT('%', U.UserName, '%'))
		AND (ISNULL(Org) OR P.OrganizationId = Org)
   GROUP BY ProjectId,
			P.Description;
            
END