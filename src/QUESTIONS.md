# QUESTIONS
## Congestion Tax Calculator

What would be the evolution of the API in terms of contracts/definitions of what to query for?


My primary focus was to get some tests wraped around the logic.
I know some test to not pass ATM, but I tried to setup

Have I missed anything special about the post-it dates?\
I tried to cover as much of the policies I could find, but maybe some was missed.

I did not have time to do a propper impl of the sorting of which passages to include.\
This also does not cover that the time diff doesnt work as expected.

A first brute solution would be to list all dates and rates and then to reduce until no entries have any greater fees within the timeframe.\

I did a start of moving the fee config to external, but no "factory" or abstraction added to support this using DI.