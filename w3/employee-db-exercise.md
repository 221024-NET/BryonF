# Employee DB Exercise
## Use the associated ERD for reference.

## Setup
In a new database, create the three following tables with the following fields. Use your best judgement to decide what data types each field will require.

### Employee
- ID
- FirstName
- LastName
- SSN (Social Security Number, traditionally 10 digits long)
- DeptID

### Department
- ID
- Name
- Location

### EmpDetails
- ID
- EmployeeID
- Salary
- Address1
- Address2
- City
- State
- Country

## References
Update the three tables with the following references:
- EmpDetails references Employee with a 1 - 1 relationship, using Employee.ID and EmpDetails.EmployeeID
- Employee references Departmet with a many - 1 relationship, using Employee.DeptD and Department.ID **(Remember, order matters for 1 - many or many - 1 relationships)**

## Challenges
Once your database is built and has the previously described relationships, complete the steps below.

- Add at least 3 records into each table.
- Add an Employee named Tina Smith.
- Add a Department named Marketing.
- List all employees in the Marketing department.
- Report the total salary of all employees in the Marketing department.
- Report the total of employees by department.
- Increase salary of Tina Smith to $90,000.
