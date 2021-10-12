Time Duration Used = two and half day. Start from Sunday (10/10/2021) Morning until Tuesday (12/10/2021) Afternoon.

Compiler - Visual Studio 2019 (.net 4.7) console Application 


Calculator -Ideagen\bin\Debug inside has Exe file to execute.

SumString has been tested :
1 + 2
2 * 2
1 + 2 + 3
6 / 2
11 + 23
11.1 + 23
1 + 2 * 3 * 3 + 3
3 * 3 * 3
11.1 + 23 + 3 / 3 * 3 + 1
( 1 + 2 ) + 5 * 2
( 11.5 + 15.4 ) + 10.1 + 2 * 5
23 - ( 29.3 - 12.5 )
( 1 / 2 ) - 1 + 1
10 - ( 2 + 3 * ( 7 - 5 ) ) 
10 - ( 2 + 3 * ( 7 - 5 ) ) + 2 * 3 


Process Flow please refer to the 

"Diagram For the Calculator Process Flow.png"



Assumption:
1) the operator (+-*/) total must be lesser than the number.
2) if there has any operation in between of two close bracket will not work that +9 will not working eg: 10 - ( 2 + 3 * ( 7 - 5 ) + 9 ). i assumed all the opertor will request before close bracket / open bracket.
3) bracket without operation will not work eg ( 3 ) + ( 2 ) or the value will be wrong.