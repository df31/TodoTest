@AddTask
Feature: AddTask
		User want to add new task to todo list

Scenario: Add new Task
	Given user wants to add new task
	When the request is sent
	Then the task is added