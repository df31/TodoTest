@GetTask
Feature: GetTask
	User want to get task from todo list

Scenario: Get all the tasks
	Given user wants to get all the task
	When the request is sent
	Then the result should be fetched

Scenario Outline: Get specific user's tasks
	Given <user> tasks is wanted
	When the request is sent
	Then The user's tasks should be fetched

	Examples:
		| user |
		| 1    |
		| 2    |
