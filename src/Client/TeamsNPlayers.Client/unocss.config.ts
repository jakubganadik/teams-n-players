import {
  defineConfig,
  presetAttributify,
  presetIcons,
  presetUno,
  presetWebFonts,
  transformerDirectives,
  transformerVariantGroup,
} from 'unocss'

import transformerDirective from '@unocss/transformer-directives'
import { presetScrollbar } from 'unocss-preset-scrollbar'
import { presetForms } from '@julr/unocss-preset-forms'
import { themeColors } from './material-design'

export default defineConfig({
  shortcuts: [
    [/^icon-(.+)$/, ([_, group1]) => `block i-material-symbols-${group1}`],
    [/^elevation-([1-5])$/, ([_, group1]) => `relative before:content-empty before:absolute before:inset-0 before:bg-primary before:pointer-events-none before:opacity-${[5, 8, 11, 12, 14][+group1!-1]}`],
  ],
  presets: [
    presetUno(),
    presetIcons({
      scale: 1.2,
      warn: true,
    }),
    presetWebFonts({
      fonts: {
        sans: 'DM Sans',
        serif: 'DM Serif Display',
        mono: 'DM Mono',
      },
    }),
    presetScrollbar({}),
    presetForms()
  ],
  theme: {
    colors: {
      ...themeColors()
    }
  },
  transformers: [
    transformerDirectives(),
    transformerVariantGroup(),
    transformerDirective(),
  ],
})
