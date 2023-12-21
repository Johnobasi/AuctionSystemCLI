![bid](https://github.com/Johnobasi/AuctionSystemCLI/assets/33833183/71980c59-a7ab-47bc-aa1b-15027f48e8ca)


Process of the AuctionSystemCLI:

Initialization:

The application starts by initializing core components: RPCServer, RPCClient, and AuctionManager.
RPCServer starts to handle remote procedure calls (RPCs).

User Interaction Loop:

The application enters a loop to interact with the user through the console.
Options are provided: Start an auction, Place a bid, View auctions, or Exit.

Starting an Auction:

User initiates an auction by providing item details and a starting price.
AuctionManager creates a new auction with the given details.
The auction is added to the list of auctions, and a notification is broadcasted to all participants using RPCClient.

Placing a Bid:

User places a bid by providing the auction ID, bid amount, and bidder name.
AuctionManager validates the auction ID and ensures the bid is higher than the current highest bid.
If the bid is valid, it's accepted, updating the auction's highest bid, and a bid notification is sent via RPCClient.

Viewing Auctions:

User chooses to view auctions.
All available auctions with their IDs, item details, current highest bid, and winner (if any) are displayed.

Concluding an Auction:

When the auction's closing time arrives or it's manually finalized by a user:

AuctionManager finalizes the auction by specifying the final amount and the winner.
Finalization involves updating the auction's status, setting the final amount, and determining the winner based on the highest bid.
Once finalized, a notification containing the auction's outcome (final amount and winner) is broadcasted to all participants using RPCClient.

Closing the Application:

User chooses to exit the application.
The RPCServer is stopped, and the program terminates.
The deeper functionality of concluding an auction includes the following steps:

Finalizing the Auction:

The process of closing an auction involves setting the final amount and determining the winner.
AuctionManager verifies the auction status, calculates the final amount, and identifies the highest bidder as the winner.
After finalization, the auction status is updated, and winner details are recorded.
