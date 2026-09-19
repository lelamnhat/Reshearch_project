class Car:
    def __init__(self, brand, model, year):
        self.brand = brand
        self.model = model
        self.year = year

    def description(self):
        print("Thông tin xe:")
        print("Brand:", self.brand)
        print("Model:", self.model)
        print("Year:", self.year)


# Chương trình chính
brand = input("Nhập hãng xe: ")
model = input("Nhập mẫu xe: ")
year = int(input("Nhập năm sản xuất: "))

car = Car(brand, model, year)

car.description()