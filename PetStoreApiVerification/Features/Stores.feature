Feature: Stores
	Access to Petstore orders

Background:
	Given I will call the PetStore base uri

Scenario: Place an order for a PET
	When I perform POST operation to place the order for pet
		| id | petId | quantity | status | complete |
		| 1  | 1     | 10       | placed | true     |
	Then I should see the response as successful with status code as 200 ok

Scenario Outline: Find purchase order by ID
	When I perform GET operation to find purchase order by ID as <ID>
	Then  I should see the response with different status code <statuscode>

	Examples:
		| ID    | statuscode |
		| 1     | 200        |
		| 11    | 404        |