"use client"
import React from 'react'
import AppointmentsDash from './dashboardCards/AppoinmentsDash';
import ExpensesDash from './dashboardCards/ExpensesDash';
import FollowersDash from './dashboardCards/FollowersDash';
import ReviewsDash from './dashboardCards/ReviewsDash';
import MessagesDash from './dashboardCards/MessagesDash';
import { createInstance } from '@/utils/axiosConfig';
import { useFetchDataVet } from '@/app/hooks/useDashboard';
import { AxiosInstance } from 'axios';
import { Appointment } from '@/types';

//TODO: completar con api calls

type Props = {
  token: string;
}
export const VetMain = ({ token }: Props) => {
  let axiosI: AxiosInstance = createInstance(token);

  const { data, isLoading, isError } = useFetchDataVet(axiosI);

  if (isLoading) return <div>Loading</div>

  if (isError) return <div>Error...</div>

  let appointments: Appointment = data.result.appointments;
  let expenses = data.result.expensesVet;
  let messages = data.result.messages;
  let reviews = data.result.reviews;

  return (
    <div className="flex justify-between flex-wrap w-[80vw] min-h-full">
      <AppointmentsDash data={appointments} />
      <ReviewsDash data={reviews} />
      <ExpensesDash data={expenses} />
      <MessagesDash data={messages} />
      <FollowersDash />
    </div>
  )
}
