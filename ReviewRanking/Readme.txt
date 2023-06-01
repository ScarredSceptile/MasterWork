How to use the program:
	Run ReviewRanking.exe (Only confirmed to work on windows. Unlikely to work on MAC/Linux)
	On the bottom, if the database is not connected, you can click the "Change DB" button to select a DB.
		The program expects a .sqlite database
	When connected to a database, both the "Start ranking with selected course" and course dropdown will be available.
		Start ranking starts the ranking
		Course dropdown allows you to select which course to rank, with amount of reviews in the course

Once ranking has started, you will have 2 large textfields which contains 2 different reviews.
Between the reviews, there are 3 button which use crocodile mouths for selecting a better review.
Underneath the buttons, the current ranking method is shows:
	RankByGroup: Ranks reviews based on which group they are in
	RankBetweenGroups: Selects the best, worse and middle review in each group, and compares them to each other
	FinishRanking: Compares reviews that has yet to get a comparison in the previous two methods in order to finish the ranking
Underneath the ranking method, the current amount of comparisons for the current ranking is shown
At the bottom is the current progress in the ranking method, like how many groups are compared, if applicable to the method.

Structure of the database:
Tables:
	TextReviews
		ID			|	INTEGER	NOT NULL UNIQUE
		Review		|	TEXT NOT NULL
		Course		|	INTEGER NOT NULL
		Score		|	INTEGER NOT NULL DEFAULT 0	|	OBSOLETE - Currently not used, but still remaining in database
		Grouping	|	INTEGER NOT NULL DEFAULT 0
	CourseReviews
		ID			|	INTEGER NOT NULL UNIQUE
		Course		|	INTEGER NOT NULL
		Reviews		|	INTEGER NOT NULL
	Courses
		ID			|	INTEGER NOT NULL UNIQUE
		Name		|	TEXT NOT NULL
		Code		|	TEXT NOT NULL

Course and grouping are the columns which decide which reviews compare to what and the order of how they are compared.
Reviews with different Course are NEVER compared.
Reviews with the same Course but different Grouping will be compared in the RankBetweenGroups and FinishRanking methods.
Reviews with the same Course and Grouping will be compared fully in the RankByGroup method.

Currently, the groupings are set to for each course to be approximately between 60 and 80 reviews, with some having less while some have more

Note: There used to be an url here for the version that was used during the thesis, but it was a private url.