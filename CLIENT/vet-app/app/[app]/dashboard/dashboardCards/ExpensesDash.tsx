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

function ExpensesDash() {
  return (
    <Card className="w-[45vw] h-[35vh]">
      <CardHeader>
        <CardTitle>
          <span>Últimos gastos</span>
        </CardTitle>
      </CardHeader>
      <CardContent>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead>Fecha</TableHead>
              <TableHead>Monto</TableHead>
              <TableHead>Descripción</TableHead>
              <TableHead>Categoría</TableHead>
              <TableHead>Extraordinario</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            <TableRow>
              <TableCell>01/01/2024</TableCell>
              <TableCell>19:30</TableCell>
              <TableCell>Ejemplo descripcion</TableCell>
              <TableCell>Ejemplo</TableCell>
              <TableCell>S</TableCell>
              <TableCell className="text-right">
                <Button className="mr-2">E</Button>
                <Button>B</Button>
              </TableCell>
            </TableRow>
            <TableRow>
              <TableCell>01/01/2024</TableCell>
              <TableCell>19:30</TableCell>
              <TableCell>Ejemplo</TableCell>
              <TableCell>S</TableCell>
              <TableCell className="text-right">
                <Button className="mr-2">E</Button>
                <Button>B</Button>
              </TableCell>
            </TableRow>
            <TableRow>
              <TableCell>01/01/2024</TableCell>
              <TableCell>19:30</TableCell>
              <TableCell>Ejemplo</TableCell>
              <TableCell>N</TableCell>
              <TableCell className="text-right">
                <Button className="mr-2">E</Button>
                <Button>B</Button>
              </TableCell>
            </TableRow>
            <TableRow>
              <TableCell>01/01/2024</TableCell>
              <TableCell>19:30</TableCell>
              <TableCell>Ejemplo</TableCell>
              <TableCell className="text-center">S</TableCell>
              <TableCell className="text-right">
                <Button className="mr-2">E</Button>
                <Button>B</Button>
              </TableCell>
            </TableRow>

          </TableBody>
        </Table>
      </CardContent>
    </Card>
  )
}


export default ExpensesDash;

