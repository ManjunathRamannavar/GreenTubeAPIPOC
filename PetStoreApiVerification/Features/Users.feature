Feature: Users
	Operations about user

Background:
	Given I will call the PetStore base uri

Scenario: Creating user
	When I perform POST operation for User
	Then I should see the response as successful with status code as 200 ok

Scenario: Creating user with table
	When I perform POST operation for User with Table data
		| id | username | firstName | lastName | email           | password | phone       | userStatus |
		| 4  | Mike K   | Mike      | K        | mikek@gmail.com | mike123  | 98765434456 | 10         |
	Then I should see the response as successful with status code as 200 ok

Scenario Outline: Updating user details with PUT operation
	When I perform PUT operation for User to update details of username "<username>"
		| id | username | firstName | lastName | email          | password | phone       | userStatus |
		| 5  | Mike K   | Mike     | k         | Mike@gmail.com | Mike123  | 98765434456 | 5          |
	Then I should see the response with status code <statuscode>

	Examples:
		| username | statuscode |
		| Mike K   | 200        |
		|          | 405        |

Scenario Outline:Deleting the user by Username using DELETE operation
	When I perform Delete operation for User By providing username "<username>"
	Then I should see the response as successfully deleted and status code <statuscode>

	Examples:
		| username   | statuscode |
		| Suresh Raj | 200        |
		| Suresh     | 404        |
		|            | 405        |

Scenario Outline:Verify the GET operation with different usernames and status codes
	When I perform GET operation for  User by providing username "<username>"
	Then I should see the response with status code <statuscode>

	Examples:
		| username | statuscode |
		| Mike K   | 200        |
		| Suresh   | 404        |
		|          | 405        |

Scenario Outline: Create User by POST and Verify by GET operation
	When I perform POST operation for User with examples  "<id>","<username>","<firstName>","<lastName>","<email>","<password>","<phone>","<userstatus>"
	And I perform GET operation for  User by providing username "<username>"
	Then I should see that the record created with the POST matches with the response of the GET with status code <statuscode>

	Examples:
		| id | username | firstName | lastName | email           | password | phone        | userstatus | statuscode |
		| 20 | Johnson  | John      | R        | john@gmail.com  | john123  | 987643434456 | 10         | 200        |


	
	
