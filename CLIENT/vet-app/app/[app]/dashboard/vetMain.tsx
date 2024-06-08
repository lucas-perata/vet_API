"use client"
import React from 'react'
import AppointmentsDash from './dashboardCards/AppoinmentsDash';
import ExpensesDash from './dashboardCards/ExpensesDash';
import FollowersDash from './dashboardCards/FollowersDash';
import ReviewsDash from './dashboardCards/ReviewsDash';
import MessagesDash from './dashboardCards/MessagesDash';

//TODO: completar con api calls

type Props = {
  token: string;
}
export const VetMain = ({ token }: Props) => {
  return (
    <div className="flex justify-between flex-wrap w-[80vw] min-h-full">
      <AppointmentsDash />
      <ReviewsDash />
      <ExpensesDash />
      <MessagesDash />
      <FollowersDash />
    </div>
  )
}
