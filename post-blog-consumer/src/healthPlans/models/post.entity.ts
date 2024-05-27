import { Column, Entity, PrimaryGeneratedColumn } from 'typeorm';

@Entity('healthPlan_post')
export class HealthPlanPostEntity {
  @PrimaryGeneratedColumn()
  id: number;

  @Column({
    default: '',
  })
  name: string;

  @Column({
    default: 0,
  })
  price: number;

  @Column({
    default: '',
  })
  details: string;

  @Column({
    default: 0,
  })
  duration: number;

  @Column({
    type: 'timestamp',
    default: () => 'CURRENT_TIMESTAMP',
  })
  createdAt: Date;
}
