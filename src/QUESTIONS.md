# QUESTIONS
## Congestion Tax Calculator

What would be the evolution of the API in terms of contracts/definitions of what to query for?

Im a bit consused about the rules for timeframes...\
In my mind Im thinking that the passages needs to be buffered to check highest passage for each hour (as of now)\

For example passages:
 - 06:15 (First and cheapest)
 - 06:35 (At this point the more expensicve overriding the 06:15)
 - 07:30 (AThe most expensive overriding the 06:35 AND should "re-activate" the 06:15 as a passage of its own)


My primary focus was to get some tests wraped around the logic.
I know some test to not pass ATM, but I tried to setup

Have I missed anything special about the post-it dates?\
I tried to cover as much of the policies I could find, but maybe some was missed.

I did not have time to do a propper impl of the sorting of which passages to include.\
This also does not cover that the time diff doesnt work as expected.

A first brute solution would be to list all dates and rates and then to reduce until no entries have any greater fees within the timeframe.\

I did a start of moving the fee config to external, but no "factory" or abstraction added to support this using DI.

