const prepend = (prefix: string) => (strings: string[]) => strings.flatMap(s => [s, `${prefix}${s}`])
const append = (suffix: string) => (strings: string[]) => strings.flatMap(s => [s, `${s}${suffix}`])

const on = prepend('on-')
const container = append('-container')

const colors = [
  ...on(container(['primary', 'secondary', 'tertiary', 'error'])),
  ...on(['background', 'surface', 'surface-variant']),
  'outline', 'shadow'
]

console.log(colors)

export const themeColors = () => Object.fromEntries(colors.map(c => [c.replace(/-(.)/g, ([_, group1]) => group1.toUpperCase()), `var(--md-sys-color-${c})`]))

console.log(themeColors())

