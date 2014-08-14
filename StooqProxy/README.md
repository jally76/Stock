StooqProxy
=====

Story:

	It's nearly impossible to get a precise intraday data about Warsaw Stock Exchange I decided to code proxy/wrapper which reads data from Stooq.com.

	A first step is to read archive data with daily interval, the second step is parsing and capturing stock prices live.

Methodology:

	TDD is used.

	Notation rules as follows:
		Interfaces prefix:	I
		Models/POCOs prefix:	M
		Tests prefix:	T
		BLL prefix:	L