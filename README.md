# Students
A progess test with C#


Progress test 1

The program reads student data from a text file, calculates the course grades using that data, and displays the results.  Classes Student, GraduateStudent, UndergraduateStudent, and ComputeGrades are used.
Student is an abstract class representing a student.  It stores the information about a student’s grade and scores of three tests.  Its specification is given below:
・	Method getTestScore returns the test score of the given test number.
・	Method setTestScore sets the test score of the given test number.
・	Abstract method computeCourseGrade computes the course grade from the test scores.  The formulas for the grade calculation are given by subclass implementation.
Class GraduateStudent is a subclass of Student with the computeCourseGrade implementation for graduate students.
Class UndergraduateStudent is a subclass of Student with the computeCourseGrade implementation for undergraduate students.
Class NameComparer will implement Icomparer interface to compare two Students by his/her name.
Class ComputeGrades will make use of the four classes given above (Student, GraduateStudent, UndergraduateStudent, and NameComparer).  This ComputeGrades class will read students data from a text file, compute the course grades, sort the student’s list  by the name order, and display the results.  The format of the input file is given below:
Column	Description
1	Student type: G for graduates, U for undergraduates
2	Student first name
3	Student last name
4	Score of test-1
5	Score of test-2
6	Score of test-3
The columns are separated by one whitespace character.  Sample data from the text file and its execution results are as follows.
Sample data:		
U  John Doe  76  65  75
G  Jill Hil  78  69  78
Execution results:	
The Student’s list:
U John Doe  76  65  75  Pass
G Jill Hil  78  69  78  No Pass

The Student’s list sorted by the name order:
G Jill Hil  78  69  78  No Pass
U John Doe  76  65  75  Pass
