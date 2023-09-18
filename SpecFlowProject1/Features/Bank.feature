Feature: Creating And Deleting Account in Bank

Background:
    Given Pick TestData, PostURL And retrive test data in json format 

@Sanity
Scenario: Create Account
	 
	Given Post the request "filePath"
	When Read data "content"
	Then Validate data "Data"

Scenario: Delete Account
	 
	Given Post the delete request "filePathdelete"
	When Read deleted data  "contentdelete"
	Then Validate deleted data "Datadelete"

