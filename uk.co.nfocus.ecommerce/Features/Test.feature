@RunThis @GUI
Feature: Discount Application and Order Confirmation 

A short summary of the feature
Background: 
Given I am logged in and have the cap in my basket

Scenario: Adding discount
//Given I am logged in and have the cap in my basket
//When I go to the checkout and add the discount code
Given I am on the Cart Page
When I apply a discount code
Then it should reduce the cost when applied

Scenario: Post Order Details when Logged In
Given I have placed an order 
When it is completed
Then I am given a order number
Then it matches the order in the top of my account
