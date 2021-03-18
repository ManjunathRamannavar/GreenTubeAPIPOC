Feature: Pet
	Everything about Pets

	Background:
	Given I will call the PetStore base uri

Scenario: Add a new pet to the store
	When I perform POST operation for PET
	Then I should see the response as successful with status code

Scenario Outline:Verify the GET operation with different PETID and status codes
	When I perform GET operation for PET by providing Pet ID <petID>
	Then I should see the response with different status code <statuscode> 

	Examples:
		| petID | statuscode |
		| 20    | 200        |
		| 500   | 404        |
	
	Scenario: Creates list of users with given input array
	When I perform POST operation for "/store/order" 
	Then I should see the response as successful with status code

	Scenario: Find purchase order by ID
	When I perform GET operation for "/store/order/{orderId}"  with orderId "5"
	Then I should see the response as successful with status code