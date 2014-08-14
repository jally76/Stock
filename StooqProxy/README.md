StooqProxy
=====

<b>Story</b>

It's nearly impossible to get a precise intraday data about Warsaw Stock Exchange so I decided to code proxy/wrapper which reads data from Stooq.com.

A first step is to read archive data with daily interval, the second step is parsing and capturing stock prices live.

<b>Methodology:</b>

TDD is used.

Notation rules as follows:
<ul>
	<li>Interfaces prefix:	I</li>
	<li>Models/POCOs prefix:	M</li>
	<li>Tests prefix:	T</li>
	<li>BLL prefix:	L</li>
</uL>