import React, { useState, useEffect } from 'react';
import {
  DataGrid,
  Editing,
  Column,
  Popup,
  Form
} from "devextreme-react/data-grid";
import "devextreme/dist/css/dx.light.css";
import DashboardCard from 'src/components/shared/DashboardCard';
import { useSalarios } from './salariosContext';

const GridSalarios = () => {
    const { store } = useSalarios();
    
    return (
        <DashboardCard title="Tabla Salarios">
            <DataGrid 
                dataSource={store}
                showBorders={true}
                repaintChangesOnly={true}
                height={600}
            >
                <Editing 
                    mode="popup"
                    allowAdding={true}
                    allowDeleting={true}
                    allowUpdating={true}
                    form={{
                        items: [
                            {
                                dataField: 'monto'
                            },
                            {
                                dataField: 'fecha'
                            },
                            {
                                dataField: 'userId'
                            }
                        ]
                    }}
                    
                >

                    <Popup title="Edit Salario" showTitle={true} width={700} height={525} />

                </Editing>
                <Column dataField="id" caption="Id" allowEditing={false}/>
                <Column dataField="monto" caption="Monto" />
                <Column 
                    dataField="fecha" 
                    caption="Date" 
                    dataType="date" 
                    format="yyyy-MM-dd"
                />
                <Column 
                    dataField="names" 
                    caption="Names"
                />
                <Column 
                    dataField="lastName" 
                    caption="Last Name"
                />
                <Column 
                    dataField="secondLastName" 
                    caption="Second Last Name"
                />
            </DataGrid>
        </DashboardCard>
    );
};

export default GridSalarios;