import { MedicineShopPost } from './../models/post.interface';
import { Controller, Inject } from '@nestjs/common';
import { EventPattern, MessagePattern } from '@nestjs/microservices';
import { MedicineShopService } from '../services/medicineShop.service';

@Controller()
export class MedicineShopController {
  constructor(private medicineShopService: MedicineShopService) {}

  @MessagePattern('get-medicine')
  async Medicines(data: any) {
    const medicines = await this.medicineShopService.findAll();
    await this.medicineShopService.sendData(medicines);
  }

  @MessagePattern('get-medicine-by-id')
  async MedicineById(data: any) {
    const medicine = await this.medicineShopService.findOne(data.value);
    await this.medicineShopService.sendData(medicine);
  }

  @MessagePattern('post-medicine')
  async addMedicine(Data: any) {
    const medicineShopPost : MedicineShopPost = {
      name: Data.Name,
      quantity: Data.Quantity,
      price: Data.Price,
    };
    const medicine = await this.medicineShopService.create(medicineShopPost);
    await this.medicineShopService.sendData(medicine);
  }

  @MessagePattern('put-medicine')
  async updateMedicine(data: any) {
    console.log(data);
    const medicine = await this.medicineShopService.update(data.id, data.data.value);
    await this.medicineShopService.sendData(medicine);
  }

  @MessagePattern('delete-medicine')
  async deleteMedicine(data: any) {
    const medicine = await this.medicineShopService.delete(data.value);
    await this.medicineShopService.sendData(medicine);
  }
}
