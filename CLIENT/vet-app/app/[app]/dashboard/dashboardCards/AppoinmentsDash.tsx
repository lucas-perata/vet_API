import { Button } from '@/components/ui/button'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table"
import { Appointment } from '@/types';
import React from 'react'

function AppointmentsDash(data) {
  return (
    <Card className="w-[43vw] h-[35vh]">

      <CardHeader>
        <CardTitle>
          Próximas citas confirmadas
        </CardTitle>
      </CardHeader>
      <CardContent>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead>Fecha</TableHead>
              <TableHead>Razón</TableHead>
              <TableHead className="text-right">Mascota</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>

            {
              data.data.map((appointment: Appointment) => (
                <TableRow>
                  <TableCell>{appointment.date}</TableCell>
                  <TableCell>{appointment.motive}</TableCell>
                  <TableCell className="text-center">{appointment.petId}</TableCell>
                  <TableCell className="text-right">
                    <Button className="mr-2">C</Button>
                    <Button>M</Button>
                  </TableCell>
                </TableRow>
              ))
            }
          </TableBody>
        </Table>

      </CardContent>
    </Card>
  )
}


export default AppointmentsDash

