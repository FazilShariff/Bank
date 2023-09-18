Feature: Creation And Deletion Of Accounts In CentralizedBank

Background:
    Given I have testdatafile for creation/deletion of account and baseUrl 

@Sanity
Scenario: Creation of bank Accounts from CentralizedBank
	 
	When Post call is made to create an account
	Then I get successful response code 200
	And I validate the successful response message as "Account created"
	And Account created with initial balance '1000'
	
	

Scenario: Deletion of bank Accounts from CentralizedBank
	 
	When Delete call is made to delete an account
	Then I get successful response code 200
	And I validate the successful response message as "Account deleted"
	And User got deleted from the Accounts 




