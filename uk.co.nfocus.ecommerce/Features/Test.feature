@RunThis @GUI
Feature: Discount Application

A short summary of the feature

@tag1
Scenario: Adding discount
Given I am logged in and have the cap in my basket
When I go to the checkout and add the discount code
Then should reduce the cost when applied

Scenario: Post Order Details when Logged In
Given I have placed an order 
When it is completed
Then I am given a order number
Then it matches the order in the top of my account
