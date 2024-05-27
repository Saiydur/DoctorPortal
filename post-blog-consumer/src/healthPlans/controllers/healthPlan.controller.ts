import {
  Controller,
  Get,
  Post,
  Body,
  Put,
  Param,
  Delete,
} from '@nestjs/common';
import { HealthPlanService } from '../services/healthPlan.service';
import { HealthPlanPost } from '../models/post.interface';
import { Observable } from 'rxjs';
import { UpdateResult } from 'typeorm/query-builder/result/UpdateResult';
import { DeleteResult } from 'typeorm';
import { MessagePattern } from '@nestjs/microservices';

@Controller('healthPlans')
export class HealthPlanController {
  constructor(private healthPlanService: HealthPlanService) {}

  @MessagePattern('get-healthPlans')
  async Medicines(data: any) {
    const medicines = await this.healthPlanService.findAll();
    await this.healthPlanService.sendData(medicines);
  }

  @MessagePattern('get-healthPlans-by-id')
  async MedicineById(data: any) {
    const medicine = await this.healthPlanService.findOne(data.value);
    await this.healthPlanService.sendData(medicine);
  }

  @MessagePattern('post-healthPlans')
  async addMedicine(Data: any) {
    const healthpost : HealthPlanPost = {
      name: Data.Name,
      prize: Data.Prize,
      details: Data.Details,
      duration: Data.Duration,
    };
    const healthplan = await this.healthPlanService.create(healthpost);
    await this.healthPlanService.sendData(healthplan);
  }

  @MessagePattern('put-healthPlans')
  async updateMedicine(data: any) {
    const hospitalpost : HealthPlanPost = {
      name: data.Name,
      prize: data.Prize,
      details: data.Details,
      duration: data.Duration,
    };
    const hospital = await this.healthPlanService.update(data.Id, hospitalpost);
    await this.healthPlanService.sendData(hospital);
  }

  @MessagePattern('delete-healthPlans')
  async deleteMedicine(data: any) {
    const medicine = await this.healthPlanService.delete(data.value);
    await this.healthPlanService.sendData(medicine);
  }
}
