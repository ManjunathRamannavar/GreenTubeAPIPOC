Feature: Stores
	Access to Petstore orders

Background:
	Given I will call the PetStore base uri

Scenario:(1) Place an order for a PET
	When I perform POST operation to place the order for pet
		| id | petId | quantity | status | complete |
		| 2  | 1     | 10       | placed | true     |
	Then I should see the response as successful with status code as 200 ok

Scenario Outline:(2) Find purchase order by ID
	When I perform GET operation to find purchase order by ID as <ID>
	Then  I should see the response with different status code <statuscode>

	Examples:
		| ID   | statuscode |
		| 2    | 200        |
		| 1111 | 404        |

Scenario Outline:(3) Create Order by POST and Verify by GET operation
	When I perform POST operation for Order with examples as "<id>","<petId>","<quantity>","<status>","<complete>"
	And I perform GET operation for  order by providing orderId <id> to check the PoST request succesfully created order
	Then I should see that the record created with the Store POST matches with the response of the store GET with status code <statuscode>

	Examples:
		| id | petId | quantity | status  | complete  | statuscode |
		| 10 | 3     | 10       | waiting | false     | 200        |