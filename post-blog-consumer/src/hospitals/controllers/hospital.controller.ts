import {
  Controller,
} from '@nestjs/common';
import { HospitalService } from '../services/hospital.service';
import { HospitalPost } from '../models/post.interface';
import { MessagePattern } from '@nestjs/microservices';

@Controller()
export class HospitalController {
  constructor(private hospitalService: HospitalService) {}

  @MessagePattern('get-hospitals')
  async Medicines(data: any) {
    const medicines = await this.hospitalService.findAll();
    await this.hospitalService.sendData(medicines);
  }

  @MessagePattern('get-hospitals-by-id')
  async MedicineById(data: any) {
    const medicine = await this.hospitalService.findOne(data.value);
    await this.hospitalService.sendData(medicine);
  }

  @MessagePattern('post-hospitals')
  async addMedicine(Data: any) {
    const hospitalpost : HospitalPost = {
      name: Data.Name,
      address: Data.Address,
    };
    const hospital = await this.hospitalService.create(hospitalpost);
    await this.hospitalService.sendData(hospital);
  }

  @MessagePattern('put-hospitals')
  async updateMedicine(data: any) {
    const hospitalpost : HospitalPost = {
      name: data.Name,
      address: data.Address,
    };
    const hospital = await this.hospitalService.update(data.Id, hospitalpost);
    await this.hospitalService.sendData(hospital);
  }

  @MessagePattern('delete-hospitals')
  async deleteMedicine(data: any) {
    const medicine = await this.hospitalService.delete(data.value);
    await this.hospitalService.sendData(medicine);
  }
}
