Feature: EditTask
	User wants to update a task


Scenario: Edit Existing Task
	Given user wants to update task
	When the request is sent
	Then the task is updated