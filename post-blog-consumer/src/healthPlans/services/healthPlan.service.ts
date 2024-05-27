import { Inject, Injectable } from '@nestjs/common';
import { DeleteResult, Repository, UpdateResult } from 'typeorm';
import { InjectRepository } from '@nestjs/typeorm';
import { Observable, from } from 'rxjs';

import { HealthPlanPostEntity } from '../models/post.entity';
import { HealthPlanPost } from '../models/post.interface';
import { ClientProxy, ClientProxyFactory, Transport } from '@nestjs/microservices';

@Injectable()
export class HealthPlanService {
  private readonly otherClient: ClientProxy;

  constructor(
    @InjectRepository(HealthPlanPostEntity)
    private readonly healthplanpostRepository: Repository<HealthPlanPostEntity>,
  ) {
    this.otherClient = ClientProxyFactory.create({
      transport: Transport.RMQ,
      options: {
        urls: ['amqp://localhost:5672'],
        queue: 'health-response',
        queueOptions: {
          durable: false,
        },
      },
    });
  }

  async findAll(): Promise<HealthPlanPost[]> {
    return await this.healthplanpostRepository.find();
  }

  async findOne(id: number): Promise<HealthPlanPost> {
    const query = this.healthplanpostRepository.createQueryBuilder('healthPlan_post');
    query.where('healthPlan_post.id = :id', { id: id });
    return await query.getOne();
  }

  async create(healthplanpost: HealthPlanPost): Promise<HealthPlanPost> {
    return await this.healthplanpostRepository.save(healthplanpost);
  }

  async update(id: number, healthplanpost: HealthPlanPost): Promise<HealthPlanPost> {
    await this.healthplanpostRepository.update(id, healthplanpost);
    return await this.findOne(id);
  }

  async delete(id: number): Promise<any> {
    return await this.healthplanpostRepository.delete(id);
  }

  async sendData(data: any): Promise<any> {
    try {
      const payload = data;
      return await this.otherClient.emit("other.created", payload).toPromise();
    }
    catch (err) {
      console.log(err);
    }
  }
}
