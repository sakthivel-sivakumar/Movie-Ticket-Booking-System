# Learning extensions and limitations

This is the baseline for adding authentication, authorization and middleware. It is not a production-ready ticketing platform.

## Deliberately excluded
- Users, password hashing, JWT and authentication
- Roles, policies and authorization
- Custom/global exception middleware
- Payments
- Advanced concurrency control, rowversion, atomic seat claims and temporary seat holds

## Important implementation notes
- Show creation generates one ShowSeat row for each physical seat on the selected screen.
- Booking checks that all requested seats exist for the show and are Available, calculates price on the server, and saves booking + seat status changes in a transaction.
- Cancellation marks the booking Cancelled and restores those show seats to Available.
- A transaction makes one booking operation all-or-nothing, but does not by itself prevent two simultaneous requests from both passing the availability check. Add atomic conditional updates/concurrency handling later.
- The simple Screen/Seat listing controller endpoints currently expose the module-wide list in this teaching starter; adapt them to filter by theatre/screen as an exercise before treating those endpoints as final.
- Add stronger FK existence checks, show overlap validation, consistent ProblemDetails responses, integration tests and production logging before deployment.
