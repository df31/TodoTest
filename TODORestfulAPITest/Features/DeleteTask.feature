Feature: DeleteTask
	User wants to delete a task

@DeleteTask
Scenario Outline: Delete task according task ID
	Given user wants to delete task <id>
	When the request is sent
	Then the task is removed
	Examples: 
	| id |
	| 1  |