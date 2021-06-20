USE [Entertainment]
GO

/****** Object:  View [dbo].[ResultsView]    Script Date: 21-06-2021 02:03:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create or Alter View [dbo].[PointsTable]
As
Select 
ROW_NUMBER()OVER(ORDER BY TeamName ASC) AS RowId,
TeamName, 
Country, 
CreatedBy as [Owner], 
MatchesPlayed, 
MatchesWon, 
MatchesDraw, 
MatchesPlayed-(MatchesWon+MatchesDraw) as MatchesLost,
MatchesWon*3 +  MatchesDraw * 1 as Points

from (select *, 
(select count(*) from Matches M1 where M1.TeamA_Id= T.TeamId or M1.TeamB_Id= T.TeamId) as MatchesPlayed,
(select count(*) from Matches M1 where M1.Winner_Id = T.TeamId and (M1.TeamA_Id= T.TeamId or M1.TeamB_Id= T.TeamId)) as MatchesWon,
(select count(*) from Matches M1 where M1.Winner_Id is NULL and (M1.TeamA_Id= T.TeamId or M1.TeamB_Id= T.TeamId)) as MatchesDraw
from Teams T) result
GO

