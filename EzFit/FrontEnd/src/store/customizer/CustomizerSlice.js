import { createSlice } from '@reduxjs/toolkit';

const initialState = {
  activeDir: 'ltr',
  activeMode: 'light', // This can be light or dark
  activeTheme: 'BLUE_THEME', // BLUE_THEME, GREEN_THEME, RED_THEME, BLACK_THEME, PURPLE_THEME, INDIGO_THEME
  SidebarWidth: 270,
  MiniSidebarWidth: 87,
  TopbarHeight: 70,
  isLayout: 'boxed', // This can be full or boxed
  isCollapse: false, // to make sidebar Mini by default
  isSidebarHover: false,
  isMobileSidebar: false,
  isHorizontal: false,
  isLanguage: 'en',
  isCardShadow: true,
  borderRadius: 7,
  role: null, // Nueva propiedad para el rol
};

export const CustomizerSlice = createSlice({
  name: 'customizer',
  initialState,
  reducers: {
    setTheme: (state, action) => {
      state.activeTheme = action.payload;
    },
    setDarkMode: (state, action) => {
      state.activeMode = action.payload;
    },
    setDir: (state, action) => {
      state.activeDir = action.payload;
    },
    setLanguage: (state, action) => {
      state.isLanguage = action.payload;
    },
    setCardShadow: (state, action) => {
      state.isCardShadow = action.payload;
    },
    toggleSidebar: (state) => {
      state.isCollapse = !state.isCollapse;
    },
    hoverSidebar: (state, action) => {
      state.isSidebarHover = action.payload;
    },
    toggleMobileSidebar: (state) => {
      state.isMobileSidebar = !state.isMobileSidebar;
    },
    toggleLayout: (state, action) => {
      state.isLayout = action.payload;
    },
    toggleHorizontal: (state, action) => {
      state.isHorizontal = action.payload;
    },
    setBorderRadius: (state, action) => {
      state.borderRadius = action.payload;
    },
    setRole: (state, action) => { // Nueva acción para establecer el rol
      state.role = action.payload;
    },
    clearRole: (state) => {
      state.role = null; // Limpia el rol en el estado
    },
  },
});

export const {
  setTheme,
  setDarkMode,
  setDir,
  toggleSidebar,
  hoverSidebar,
  toggleMobileSidebar,
  toggleLayout,
  setBorderRadius,
  toggleHorizontal,
  setLanguage,
  setCardShadow,
  setRole, // Exporta la nueva acción
  clearRole,
} = CustomizerSlice.actions;

export default CustomizerSlice.reducer;
