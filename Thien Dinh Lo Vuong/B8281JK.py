class Employee:
    def work(self):
        print("Employee is working.")


class Developer(Employee):
    def work(self):
        print("Developer is coding.")


def show_work(e):
    e.work()


# Tạo đối tượng
employee = Employee()
developer = Developer()

# Gọi show_work()
show_work(employee)
show_work(developer)