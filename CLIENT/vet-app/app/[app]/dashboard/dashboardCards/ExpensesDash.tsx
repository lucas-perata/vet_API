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
import { Expense } from '@/types'
import React from 'react'

function ExpensesDash(data) {
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

            {data.data.map((expense: Expense) => (
              <TableRow>
                <TableCell>{expense.date}</TableCell>
                <TableCell>{expense.amount}</TableCell>
                <TableCell>{expense.description}</TableCell>
                <TableCell>{expense.category}</TableCell>
                <TableCell>{expense.extra == true ? "Sí" : "No"}</TableCell>
                <TableCell className="text-right">
                  <Button className="mr-2">E</Button>
                  <Button>B</Button>
                </TableCell>
              </TableRow>

            ))}




          </TableBody>
        </Table>
      </CardContent>
    </Card>
  )
}


export default ExpensesDash;

