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
    <div className="flex justify-between gap-5 flex-wrap w-[80vw] min-h-full">
      <div className="bg-green-200 w-[40%]">
        <AppointmentsDash />
      </div>
      <div className="bg-green-400 w-96">
        <ReviewsDash />
      </div>
      <div className="bg-green-500 w-96">
        <MessagesDash />
      </div>
      <div className="bg-green-300 w-[70%]">
        <ExpensesDash />
      </div>
      <div className="bg-green-600 w-96">
        <FollowersDash />
      </div>
    </div>
  )
}
