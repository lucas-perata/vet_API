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
import React from 'react'

function AppointmentsDash() {
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
              <TableHead>Horario</TableHead>
              <TableHead>Razón</TableHead>
              <TableHead className="text-right">Mascota</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            <TableRow>
              <TableCell>01/01/2024</TableCell>
              <TableCell>19:30</TableCell>
              <TableCell>Problemas de etc etc</TableCell>
              <TableCell className="text-center">Gato</TableCell>
              <TableCell className="text-right">
                <Button className="mr-2">C</Button>
                <Button>M</Button>
              </TableCell>
            </TableRow>
            <TableRow>
              <TableCell>01/01/2024</TableCell>
              <TableCell>19:30</TableCell>
              <TableCell>Problemas de etc etc</TableCell>
              <TableCell className="text-center">Gato</TableCell>
              <TableCell className="text-right">
                <Button className="mr-2">C</Button>
                <Button>M</Button>
              </TableCell>
            </TableRow>
            <TableRow>
              <TableCell>01/01/2024</TableCell>
              <TableCell>19:30</TableCell>
              <TableCell>Problemas de etc etc</TableCell>
              <TableCell className="text-center">Gato</TableCell>
              <TableCell className="text-right">
                <Button className="mr-2">C</Button>
                <Button>M</Button>
              </TableCell>
            </TableRow>
            <TableRow>
              <TableCell>01/01/2024</TableCell>
              <TableCell>19:30</TableCell>
              <TableCell>Problemas de etc etc</TableCell>
              <TableCell className="text-center">Gato</TableCell>
              <TableCell className="text-right">
                <Button className="mr-2">C</Button>
                <Button>M</Button>
              </TableCell>
            </TableRow>

          </TableBody>
        </Table>

      </CardContent>
    </Card>
  )
}


export default AppointmentsDash

