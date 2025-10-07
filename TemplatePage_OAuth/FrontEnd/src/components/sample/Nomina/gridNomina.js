import React from 'react';
import {
  DataGrid,
  Editing,
  Column
} from "devextreme-react/data-grid";
import "devextreme/dist/css/dx.light.css";
import DashboardCard from 'src/components/shared/DashboardCard';
import { useNominaTable } from './nominaTable';
const GridNomina = () => {
  const { store  } = useNominaTable();
  
  return (
    <DashboardCard title="Tabla Nomina">
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
          />
          <Column dataField="id" caption="ID" allowEditing={false} />
      
          <Column 
          dataField="mes" 
          caption="Date" 
          dataType="date" 
          allowEditing={true} 
          format="yyyy-MM-dd"
        />
      <Column dataField="cantidad" caption="Cantidad" />
      </DataGrid>
    </DashboardCard>
  );
}

export default GridNomina;