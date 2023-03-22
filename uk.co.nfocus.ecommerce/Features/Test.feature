@RunThis @GUI
Feature: Discount Application and Order Confirmation

A short summary of the feature
Background:
	Given I am logged in

Scenario: Adding discount
	Given I have <item> in my basket
	Given I am on the Cart Page
	When I apply a discount code <code>
	Then it should reduce the cost when applied by <amount>
Examples:
	| item | code      | amount |
	| cap  | edgewords | 15     |




Scenario: Post Order Details when Logged In
	Given I have placed an order containing atleast a <item>
	When it is completed
	Then I am given a order number
	Then it matches the order in the top of my account
Examples:
	| item |
	| cap  |
