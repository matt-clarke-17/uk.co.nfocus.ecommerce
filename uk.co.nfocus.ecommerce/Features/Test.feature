@RunThis
Feature: Discount Application

A short summary of the feature

@tag1
Scenario: Click on Login
	Given I am on Edgewords
	When I click on Log In
	Then I am taken to the login page

Scenario: Insert Login Details
	Given I am on the login page
	When I click on the boxes to fill my login details
	Then I can log in

Scenario: Go to Shop
Given I am logged in
When I click on shop
Then I will be taken to the shop page

Scenario: Add Item to Basket
Given I am logged in and on the shop
When I search for the cap
Then I will be able to add it to the basket

Scenario: Adding discount
Given I am logged in and have the cap in my basket
When I go to the checkout and add the discount code
Then should reduce the cost when applied

Scenario: Placing an order
Given I am logged in and have the cap in my basket
When I insert my billing details in the check out
Then I will be able to select cheque payments and place my order

Scenario: Post Order Details when Logged In
Given I have placed an order 
When it is completed
Then I am given a order number

Scenario: Post Order confirm order number match
Given I have placed an order
When it is completed and i have a order number
Then the order number in my account matches it 




