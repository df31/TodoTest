Feature: TodoApp
	Simple Todo list for tracking tasks

@Positive
Scenario Outline: Add one task to Todo List
	Given <Task> is typed in
	When Enter is pressed
	Then The task is added

	Examples:
		| Task |
		| abc  |
		| 123  |
		| #%&  |
		| 吃早饭  |

@Negative
Scenario Outline: Add an Invalid task
	Given <Task> is typed in
	When Enter is pressed
	Then The task is NOT Added

	Examples:
		| Task |
		|      |

Scenario: Remove a task
	Given One task is added
	When Click on cross button
	Then The task is removed

Scenario: Edit task by double click on it
	Given One task is added
	When Double click the task
	Then The task updated

Scenario: Mark a task as completed
	Given One task is added
	When Mark the task
	Then The task is marked

Scenario: Unmark a completed task
	Given One task is marked as completed
	When unmark the task
	Then The task is unmarked

Scenario: Remove a completed task
Given One task is marked as completed
When Click Clear Completed button
Then The task is removed

Scenario: Mark then Unmark task
Given One task is added
When Click on MarkUnmark
Then The task is marked
When Click on MarkUnmark
Then The task is unmarked
