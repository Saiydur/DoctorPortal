import { Injectable } from '@nestjs/common';
import { Repository, } from 'typeorm';
import { InjectRepository } from '@nestjs/typeorm';

import { HospitalPostEntity } from '../models/post.entity';
import { HospitalPost } from '../models/post.interface';
import {
  ClientProxy,
  ClientProxyFactory,
  Transport,
} from '@nestjs/microservices';

@Injectable()
export class HospitalService {
  private readonly otherClient: ClientProxy;

  constructor(
    @InjectRepository(HospitalPostEntity)
    private readonly hospitalpostRepository: Repository<HospitalPostEntity>,
  ) {
    this.otherClient = ClientProxyFactory.create({
      transport: Transport.RMQ,
      options: {
        urls: ['amqp://localhost:5672'],
        queue: 'hospital-response',
        queueOptions: {
          durable: false,
        },
      },
    });
  }

  async findAll(): Promise<HospitalPost[]> {
    return await this.hospitalpostRepository.find();
  }

  async findOne(id: number): Promise<HospitalPost> {
    const query =this.hospitalpostRepository.createQueryBuilder('hospital_post');
    query.where('hospital_post.id = :id', { id: id });
    return await query.getOne();
  }

  async create(hospitalpost: HospitalPost): Promise<HospitalPost> {
    return await this.hospitalpostRepository.save(hospitalpost);
  }

  async update(id: number, hospitalpost: HospitalPost): Promise<HospitalPost> {
    await this.hospitalpostRepository.update(id, hospitalpost);
    return await this.findOne(id);
  }

  async delete(id: number): Promise<any> {
    return await this.hospitalpostRepository.delete(id);
  }

  async sendData(data: any): Promise<any> {
    try {
      const payload = data;
      return await this.otherClient.emit('other.created', payload).toPromise();
    } catch (err) {
      console.log(err);
    }
  }
}
