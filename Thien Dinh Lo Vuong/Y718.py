class Vehicle:
    def __init__(self, brand, model, year, speed):
        self.brand = brand
        self.model = model
        self.year = year
        self.speed = speed

    def accelerate(self):
        self.speed += 10
        print("Toc do hien tai:", self.speed)


class Car(Vehicle):
    def __init__(self, brand, model, year, speed, fuel_type):
        super().__init__(brand, model, year, speed)
        self.fuel_type = fuel_type


class Motorcycle(Vehicle):
    def wheelie(self):
        print("The motorcycle is doing a wheelie!")


# Tao doi tuong Car
car = Car("Toyota", "Camry", 2022, 60, "Xang")

print("Thong tin Car:")
print(car.brand, car.model, car.year)
print("Nhien lieu:", car.fuel_type)
car.accelerate()


# Tao doi tuong Motorcycle
motorcycle = Motorcycle("Honda", "CBR", 2023, 50)

print("\nThong tin Motorcycle:")
print(motorcycle.brand, motorcycle.model, motorcycle.year)
motorcycle.accelerate()
motorcycle.wheelie()