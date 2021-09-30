IF NOT EXISTS (Select * From sys.objects Where name ='tblAssessmentToolsCATS' )
BEGIN
CREATE TABLE tblAssessmentToolsCATS (
     DetailRowID  INT IDENTITY (1, 1) NOT NULL,
     PatientId    INT NULL,
     EncounterID  INT NULL,
     PracticeID   INT NULL,
	 
     NaturalDisaster        	SMALLINT NULL,
     Accident        			SMALLINT NULL,
     Robbed     				SMALLINT NULL,
     BeatUpInFamily          	SMALLINT NULL,
     BeatUpBySomeone           	SMALLINT NULL,
     SeeingSomeoneInFamily      SMALLINT NULL,
     SeeingSomeoneInCommunity   SMALLINT NULL,
     TouchingPrivateParts       SMALLINT NULL,
     PressuringSex           	SMALLINT NULL,
     Dying    					SMALLINT NULL,
	 HurtBadly    				SMALLINT NULL,
	 killed    					SMALLINT NULL,
	 MedicalProcedure   		SMALLINT NULL,
	 AroundWar   				SMALLINT NULL,
	 ScaryEvent    				SMALLINT NULL,
	 DescribeCANS				VARCHAR(MAX) NULL,
	 Bothering					VARCHAR(200) NULL,
	 
	 Upsetting 					SMALLINT NULL,
	 BadDreams 					SMALLINT NULL,
	 RightNow 					SMALLINT NULL,
	 Emotionally 				SMALLINT NULL,
	 PhysicalReactions 			SMALLINT NULL,
	 NotToRemember 				SMALLINT NULL,
	 Avoiding 					SMALLINT NULL,
	 ImportantPart 				SMALLINT NULL,
	 NegativeChanges 			SMALLINT NULL,
	 Thinking 					SMALLINT NULL,
	 NegativeEmotional 			SMALLINT NULL,
	 LosingInterest 			SMALLINT NULL,
	 FeelingDistant 			SMALLINT NULL,
	 PositiveFeelings 			SMALLINT NULL,
	 BeingIrritable 			SMALLINT NULL,
	 RiskyBehavior 				SMALLINT NULL,
	 OverlyAlert 				SMALLINT NULL,
	 JumpyOrEasilyStartled		SMALLINT NULL,
	 Concentration 				SMALLINT NULL,
	 Trouble 					SMALLINT NULL,
	 
	 TotalScore      			VARCHAR(200) NULL,
	 
	 Along	 					SMALLINT NULL,
	 Hobbies	 				SMALLINT NULL,
	 SchoolOrWork	 			SMALLINT NULL,
	 FamilyRelationships		SMALLINT NULL,
	 Happiness					SMALLINT NULL,
     
	 
     CreatedBy    INT NULL,
     CreatedOn    DATETIME NULL,
     ModifiedBy   INT NULL,
     ModifiedOn   DATETIME NULL,
     HTMLField    VARCHAR(MAX) NULL,
     Comments     VARCHAR(MAX) NULL,

Constraint PK_tblAssessmentToolsCATS_DetailRowID Primary Key (DetailRowID) 
);
END
GO