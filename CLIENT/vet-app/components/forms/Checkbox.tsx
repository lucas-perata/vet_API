import React from 'react'
import { UseControllerProps } from 'react-hook-form';
import { FormControl, FormField, FormItem, FormLabel } from '../ui/form';
import { Checkbox } from "@/components/ui/checkbox"

type Props = {
  label: string;
  name: string;
  form: any;
} & UseControllerProps;

export default function CheckboxForm(props: Props) {
  return (
    <div>
      <FormField control={props.form.control} name={props.name}
        render={({ field }) => (
          <FormItem>
            <FormControl>
              <Checkbox checked={field.value}
                onCheckedChange={field.onChange}
              />
            </FormControl>
            <FormLabel>
              {props.label}
            </FormLabel>
          </FormItem>
        )} />
    </div>
  );
}

