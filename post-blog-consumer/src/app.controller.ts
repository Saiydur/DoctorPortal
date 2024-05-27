import { Controller, Get, Query } from '@nestjs/common';
import { AppService } from './app.service';
import { EventPattern, MessagePattern } from '@nestjs/microservices';

@Controller()
export class AppController {
  constructor(private readonly appService: AppService) { }

  @Get()
  getHello(): string {
    return this.appService.getHello();
  }

  @EventPattern("get-user")
  async sum(Data: any) {
    console.log('Received message here from user queue:', Data);
    const changedData = "This is new data with " + Data;
    // send data to another queue
    this.appService.sendData(changedData);
  }

  @MessagePattern('other.created')
  async getUser(data: any) {
    console.log('Received message from user queue:', data)
  }
}
