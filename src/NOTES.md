# NOTES
## Congestion Tax Calculator


### Log
Prio:
 - Setup minimal API 
 - Setup some type of testing


### Thoughts

Maybe a good candidate for approval tests?\
Maybe more as a tool to check the dates and iterate for date and some known vehicle types?

Free days:\
Do a localized "banking holiday" look-up abstraction for country + maybe some other goodies?

Currency:\
Use decimal as default for any monetary stuff, or maybe een better Money-type with amount and currency.


Fix validation of time intervals\
Need to evaluate which passages to keep within a time range of the most expensive ones.

Since we are not dealing with any behavior regarding vehicles, maybe easire to just to one that holds the type to fulfill the interface and fix string bugs.



### TODOs

Fix grouping and calculating of passages to exclude.\
Need to recursive until no entries are closer that the threshold for timeframe.

Go through the dates and see if there are any more special cases, for example do some `FeeSettings.ValidFrom` and `FeeSettings.ValidTo` to enable future price corections.

Refactor domain logic code

Add date validation (all passages must be on same day) or group by day in result

Verify that correct prices are applied

SEE
https://skatteverket.se/privat/skatter/bilochtrafik/trangselskatt/goteborg.4.2b543913a42158acf80006815.html

https://www.transportstyrelsen.se/sv/vagtrafik/Trangselskatt/Trangselskatt-i-goteborg/undantag-fran-trangselskatt-i-backa/
https://www.transportstyrelsen.se/sv/vagtrafik/Trangselskatt/Trangselskatt-i-goteborg/Tider-och-belopp-i-Goteborg/dagar-da-trangselskatt-inte-tas-ut-i-goteborg/


## Bonus Scenario

Just as you finished coding, your manager shows up and tells you that the same application should be used in other cities with different tax rules. These tax rules need to be handled as content outside the application because different content editors for different cities will be in charge of keeping the parameters up to date.

Move the parameters used by the application to an outside data store of your own choice to be read during runtime by the application.

## Further questions

As you take on the assignment you will undoubtedly have questions. Please write these questions down in a file named `questions.md` and submit that along with your solution. Although we cannot answer them in order for you to complete the assignment, they will be helpful
in evaluating your solution.
